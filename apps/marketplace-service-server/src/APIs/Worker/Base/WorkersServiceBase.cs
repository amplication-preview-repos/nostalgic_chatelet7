using MarketplaceService.APIs;
using MarketplaceService.APIs.Common;
using MarketplaceService.APIs.Dtos;
using MarketplaceService.APIs.Errors;
using MarketplaceService.APIs.Extensions;
using MarketplaceService.Infrastructure;
using MarketplaceService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.APIs;

public abstract class WorkersServiceBase : IWorkersService
{
    protected readonly MarketplaceServiceDbContext _context;

    public WorkersServiceBase(MarketplaceServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Worker
    /// </summary>
    public async Task<Worker> CreateWorker(WorkerCreateInput createDto)
    {
        var worker = new WorkerDbModel
        {
            Availability = createDto.Availability,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Rating = createDto.Rating,
            Skill = createDto.Skill,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            worker.Id = createDto.Id;
        }

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<WorkerDbModel>(worker.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Worker
    /// </summary>
    public async Task DeleteWorker(WorkerWhereUniqueInput uniqueId)
    {
        var worker = await _context.Workers.FindAsync(uniqueId.Id);
        if (worker == null)
        {
            throw new NotFoundException();
        }

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Workers
    /// </summary>
    public async Task<List<Worker>> Workers(WorkerFindManyArgs findManyArgs)
    {
        var workers = await _context
            .Workers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return workers.ConvertAll(worker => worker.ToDto());
    }

    /// <summary>
    /// Meta data about Worker records
    /// </summary>
    public async Task<MetadataDto> WorkersMeta(WorkerFindManyArgs findManyArgs)
    {
        var count = await _context.Workers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Worker
    /// </summary>
    public async Task<Worker> Worker(WorkerWhereUniqueInput uniqueId)
    {
        var workers = await this.Workers(
            new WorkerFindManyArgs { Where = new WorkerWhereInput { Id = uniqueId.Id } }
        );
        var worker = workers.FirstOrDefault();
        if (worker == null)
        {
            throw new NotFoundException();
        }

        return worker;
    }

    /// <summary>
    /// Update one Worker
    /// </summary>
    public async Task UpdateWorker(WorkerWhereUniqueInput uniqueId, WorkerUpdateInput updateDto)
    {
        var worker = updateDto.ToModel(uniqueId);

        _context.Entry(worker).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Workers.Any(e => e.Id == worker.Id))
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
