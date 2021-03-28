using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secondary.Services
{
    public class GuidProvider
    {
        private readonly Lazy<Guid> _id;

        public GuidProvider() => _id = new Lazy<Guid>(Guid.NewGuid());
        public Guid Id => _id.Value;
    }
}
