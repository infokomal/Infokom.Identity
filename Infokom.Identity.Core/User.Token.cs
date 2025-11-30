using Microsoft.AspNetCore.Identity;

namespace Infokom.Identity.Core
{
	public partial class User
    {
		public class Token : IdentityUserToken<int>
		{
		}
	}


}
