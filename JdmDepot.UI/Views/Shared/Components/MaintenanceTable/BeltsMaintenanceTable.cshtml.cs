using JdmDepot.Common.DataModels;
using JdmDepot.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JdmDepot.UI.Views.Shared.Components.MaintenanceTable;

public class BeltsMaintenanceTable : PageModel
{
	public IReadOnlyCollection<BeltMaintenanceItem> BeltMaintenanceItems { get; }

	public BeltsMaintenanceTable(IReadOnlyCollection<BeltMaintenanceItem> beltMaintenanceItems)
	{
		BeltMaintenanceItems = beltMaintenanceItems;
	}

	public void OnGet()
	{
		
	}
}
