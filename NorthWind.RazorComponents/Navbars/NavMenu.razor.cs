using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.RazorComponents.Navbars
{
	public partial class NavMenu
	{
		[Parameter]
		public RenderFragment NavBarBrand { get; set; }

		[Parameter]
		public RenderFragment NavBarItems { get; set; }

		bool CollapseNavMenu { get; set; } = true;

		string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

		void ToggleNavMenu()
		{
			CollapseNavMenu = !CollapseNavMenu;
		}
	}
}
