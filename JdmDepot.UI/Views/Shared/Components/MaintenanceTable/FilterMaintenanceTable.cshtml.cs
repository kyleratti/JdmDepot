using JdmDepot.Common.DataModels;
using JdmDepot.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JdmDepot.UI.Views.Shared.Components.MaintenanceTable;

public class FilterMaintenanceTable : PageModel
{
	public IReadOnlyCollection<FilterMaintenanceItem> FilterMaintenanceItems { get; }

	public FilterMaintenanceTable(IReadOnlyCollection<FilterMaintenanceItem> filterMaintenanceItems)
	{
		FilterMaintenanceItems = filterMaintenanceItems;
	}

	public void OnGet()
	{
		
	}
}
