﻿using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface ISocialRepository
{
    public Task<Social> AddSocial(Social social);
    public Task<Social> RemoveSocial(Expression<Func<Social, bool>> predicate);
    public Task<List<Social>> GetSocials(Expression<Func<Social, bool>> predicate);
}
