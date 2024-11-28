using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class PaymentsServiceBase : IPaymentsService
{
    protected readonly MarketplaceServiceDbContext _context;

    public PaymentsServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Payment
    /// </summary>
    public async Task<Payment> CreatePayment(PaymentCreateInput createDto)
    {
        var payment = new PaymentDbModel
        {
            Amount = createDto.Amount,
            CreatedAt = createDto.CreatedAt,
            PaymentDate = createDto.PaymentDate,
            PaymentMethod = createDto.PaymentMethod,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            payment.Id = createDto.Id;
        }

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PaymentDbModel>(payment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Payment
    /// </summary>
    public async Task DeletePayment(PaymentWhereUniqueInput uniqueId)
    {
        var payment = await _context.Payments.FindAsync(uniqueId.Id);
        if (payment == null)
        {
            throw new NotFoundException();
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Payments
    /// </summary>
    public async Task<List<Payment>> Payments(PaymentFindManyArgs findManyArgs)
    {
        var payments = await _context
            .Payments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return payments.ConvertAll(payment => payment.ToDto());
    }

    /// <summary>
    /// Meta data about Payment records
    /// </summary>
    public async Task<MetadataDto> PaymentsMeta(PaymentFindManyArgs findManyArgs)
    {
        var count = await _context.Payments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Payment
    /// </summary>
    public async Task<Payment> Payment(PaymentWhereUniqueInput uniqueId)
    {
        var payments = await this.Payments(
            new PaymentFindManyArgs { Where = new PaymentWhereInput { Id = uniqueId.Id } }
        );
        var payment = payments.FirstOrDefault();
        if (payment == null)
        {
            throw new NotFoundException();
        }

        return payment;
    }

    /// <summary>
    /// Update one Payment
    /// </summary>
    public async Task UpdatePayment(PaymentWhereUniqueInput uniqueId, PaymentUpdateInput updateDto)
    {
        var payment = updateDto.ToModel(uniqueId);

        _context.Entry(payment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Payments.Any(e => e.Id == payment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
