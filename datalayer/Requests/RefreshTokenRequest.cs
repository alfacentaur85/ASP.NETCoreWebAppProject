using System;

namespace datalayer.Requests
{
    public class RefreshTokenRequest
    {

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}
