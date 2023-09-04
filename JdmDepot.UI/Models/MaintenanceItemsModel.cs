using JdmDepot.Common.DataModels;

namespace JdmDepot.UI.Models;

public record MaintenanceItemsModel(
	string Title,
	IReadOnlyCollection<MaintenanceItem> MaintenanceItems);
