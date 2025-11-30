using Microsoft.AspNetCore.Identity;

namespace Infokom.Identity.Core
{

	public partial class Role
    {
        public class Claim : IdentityRoleClaim<int>
		{
		}
	}


}
