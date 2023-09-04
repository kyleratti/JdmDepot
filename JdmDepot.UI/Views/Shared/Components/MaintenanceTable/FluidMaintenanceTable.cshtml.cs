using JdmDepot.Common.DataModels;
using JdmDepot.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JdmDepot.UI.Views.Shared.Components.MaintenanceTable;

public class FluidMaintenanceTable : PageModel
{
	public IReadOnlyCollection<FluidMaintenanceItem> FluidMaintenanceItems { get; }

	public FluidMaintenanceTable(IReadOnlyCollection<FluidMaintenanceItem> fluidMaintenanceItems)
	{
		FluidMaintenanceItems = fluidMaintenanceItems;
	}

	public void OnGet()
	{
		
	}
}
