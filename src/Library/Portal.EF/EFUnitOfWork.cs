﻿using Portal.Application.Common;

namespace Portal.EF;

public class EFUnitOfWork : UnitOfWork
{
    private readonly EFdbApplication _context;
    public EFUnitOfWork(EFdbApplication context)
    {
        _context = context;
        _context.UseSqlServer = true;
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
