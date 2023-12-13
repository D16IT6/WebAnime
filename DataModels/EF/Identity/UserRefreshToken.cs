using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.EF.Identity
{
    public class UserRefreshToken
    {
        [ForeignKey(nameof(User))]
        public int Id { get; set; }

        public Guid RefreshToken { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset ExpiredTime { get; set; }
        public virtual Users User { get; set; }

    }
}
