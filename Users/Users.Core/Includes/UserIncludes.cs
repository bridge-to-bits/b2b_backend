﻿using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Includes;

public static class UserIncludes
{
    public static readonly Expression<Func<User, object>>[] ProducerAndPerformer =
    [
        user => user.Performer,
        user => user.Producer,
    ];

    public static readonly Expression<Func<User, object>>[] Socials =
    [
        user => user.Socials,
    ];

    public static readonly Expression<Func<User, object>>[] Genres =
    [
        user => user.Socials,
    ];

    public static readonly Expression<Func<User, object>>[] AllRelations =
    [
        user => user.Performer,
        user => user.Producer,
        user => user.Socials,
        user => user.Socials,
    ];
}
