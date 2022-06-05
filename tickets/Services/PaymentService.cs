using Microsoft.EntityFrameworkCore;
using Stripe;
using tickets.Dto;
using tickets.Models;
using tickets.Models.Entities;

namespace tickets.Services;

public class PaymentService : IPaymentService
{
    private readonly TicketsContext _context;

    public PaymentService(TicketsContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<TicketsDto>> BuyTicket(int ticketTypeId,int userId)
    {
        //TODO decrease count of tickets after payment
        ServiceResponse<TicketsDto> response = new ServiceResponse<TicketsDto>();
        try
        {
            //get ticket price and eventId
            var ticketQuery = await (from t in _context.TicketTypes
                where t.Id == ticketTypeId
                select new
                {
                    Title = t.Tittle,
                    Price = t.Price,
                    EventId = t.EventId
                }).FirstOrDefaultAsync();
            //get users email and stripe token
            var userQuery = await (from user in _context.Users
                where user.Id == userId
                select new
                {
                    email = user.Email,
                    customer = user.StripeId,
                }).FirstOrDefaultAsync();
            //object to charge user
            var options = new ChargeCreateOptions
            {
                Amount = ((long) ticketQuery.Price)*100,
                Currency = "USD",
                Description = ticketQuery.Title,
                ReceiptEmail = userQuery.email,
                Customer = userQuery.customer
            };
            //stripes charge service
            var service = new ChargeService();
            //object with payment
            Payment payment = new Payment
            {
                UserId = userId,
                Time = DateTime.Now,
                Sum = ticketQuery.Price
            };
            _context.Payments.Add(payment); //add payment object to entity
            await _context.SaveChangesAsync(); //save entity to db
            int paymentId = payment.Id; //get paymentId from db
            var charge = await service.CreateAsync(options); //charge user
            payment.TransactionId = charge.Id; 
            if (charge.Status == "succeeded")
            {
                payment.Confirmed = 1;
                //genereating ticket
                response.Data = await CreateTicket(ticketTypeId, paymentId, userId, ticketQuery.EventId);
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        
        await _context.SaveChangesAsync();
        return response;

    }
    
    public async Task<ServiceResponse<CardDataDto>> AddCard(CardDto card, int userId)
    {
        ServiceResponse<CardDataDto> response = new ServiceResponse<CardDataDto>();
        try
        {
            var token = await CreateToken(card);//create token
            //getting users email and stripe customerId
            var user = await (from u in _context.Users where u.Id == userId select new{ u.StripeId, u.Email}).FirstOrDefaultAsync();
            //add card token to stripe and getting source
            var source = await UpdateCustomer(user.StripeId, token.Id);
            var pt = await _context.Users.Include(u => u.Tokens).FirstOrDefaultAsync(u => u.Id == userId);
            //save source and card data to db
            var paymentToken = new PaymentToken
            {
                Token = source.DefaultSourceId,
                CardBrand = token.Card.Brand,
                ExpMonth = (int) token.Card.ExpMonth,
                ExpYear = (int) token.Card.ExpYear,
                Last4 = int.Parse(token.Card.Last4)
            };
            pt.Tokens.Add(paymentToken);
            await _context.SaveChangesAsync();
            //return card data to the client
            response.Data = new CardDataDto
            {
                Id = paymentToken.Id,
                ExpMonth = paymentToken.ExpMonth,
                ExpYear = paymentToken.ExpYear,
                Last4 = paymentToken.Last4
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            response.Success = false;
            response.Message = e.Message;
        }

        return response;


    }

    public async Task<ServiceResponse<bool>> DeleteCard(int cardId, int userId)
    {
        ServiceResponse<bool> response = new ServiceResponse<bool>();
        try
        {
            var service = new SourceService();
            //getting customer from db
            var customer = await (from u in _context.Users where u.Id == userId select u.StripeId).FirstOrDefaultAsync();
            //getting card source from db
            var token = await (from p in _context.PaymentTokens where p.Id == cardId select p).FirstOrDefaultAsync();
            await service.DetachAsync(customer, token.Token); //deleting card from stripe
            //deleting card from db
            _context.PaymentTokens.Remove(token);
            await _context.SaveChangesAsync();
            response.Data = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;

    }

    public async Task<ServiceResponse<List<CardDataDto>>> UserCards(int userId)
    {
        ServiceResponse<List<CardDataDto>> response = new ServiceResponse<List<CardDataDto>>();
        try
        {
            var cards = (from t in await (from u in _context.Users.Include(t => t.Tokens)
                    where u.Id == userId
                    select u.Tokens.ToList()).FirstOrDefaultAsync()
                select new CardDataDto
                {
                    Id = t.Id,
                    ExpMonth = t.ExpMonth,
                    ExpYear = t.ExpYear,
                    Last4 = t.Last4

                }).ToList();
            response.Data = cards;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        
        return response;
    }
    private async Task<TicketsDto> CreateTicket(int ticketTypeId, int paymentId, int userId, int eventId)
    {
        Guid ticketId = Guid.NewGuid(); //generating new ticket uuid
        Ticket ticket = new Ticket
        {
            Id = ticketId,
            TicketTypeId = ticketTypeId,
            PaymentId = paymentId,
            UserId = userId,
            EventId = eventId
            
        };
        
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        //getting ticket from db and returning it ti the client
        var generatedTicket = await (from t in _context.Tickets where t.Id == ticketId
            join e in _context.Events on t.EventId equals e.Id
            join p in _context.Payments on t.PaymentId equals p.Id
            join tp in _context.TicketTypes on t.TicketTypeId equals tp.Id
            join pl in _context.Places on e.PlaceId equals pl.Id
            select new TicketsDto
            {
                Id=t.Id,
                EventId=e.Id,
                EventTittle=e.Tittle,
                EventCity=pl.City,
                EventPlace=pl.Tittle,
                EventAddress=pl.Address,
                EventDate=e.Date,
                PurchaseDate = p.Time,
                IsPaid=p.Confirmed
            }).FirstOrDefaultAsync();
        return generatedTicket;
    }
    
    private async Task<Token> CreateToken(CardDto card) //create card token
    {
        var options = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Number = card.Number,
                ExpMonth = card.ExpMonth,
                ExpYear = card.ExpYear,
                Cvc = card.Cvc
            },
        };
        var service = new TokenService();
        var token = await service.CreateAsync(options);
        return token;

    }

    private async Task<Customer> UpdateCustomer(string customerId, string token) //add card token to stripe
    {
        var options = new CustomerUpdateOptions
        {
            Source = token,
        };
        var service = new CustomerService();
        var customer = await service.UpdateAsync(customerId, options);
        return customer;
    }
    
    
}


