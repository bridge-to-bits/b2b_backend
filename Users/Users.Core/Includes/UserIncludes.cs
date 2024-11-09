using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Includes;

public static class UserIncludes
{
    public static readonly Expression<Func<User, object>>[] ProducerAndPerformer =
    [
        user => user.Performer,
        user => user.Producer
    ];
}
