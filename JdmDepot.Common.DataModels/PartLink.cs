namespace JdmDepot.Common.DataModels;

public abstract record PartLink
{
	public abstract string GetUrl();

	public T Merge<T>(
		Func<AdvanceAutoPartLink, T> advanceAuto,
		Func<AmazonPartLink, T> amazon,
		Func<RockAutoPartLink, T> rockAuto) => this switch
	{
		AdvanceAutoPartLink x => advanceAuto(x),
		AmazonPartLink x => amazon(x),
		RockAutoPartLink x => rockAuto(x),
		_ => throw new NotImplementedException(GetType().FullName),
	};
}

public record AdvanceAutoPartLink(string PartNumber) : PartLink
{
	public override string GetUrl() => $"https://shop.advanceautoparts.com/web/SearchResults?searchTerm={PartNumber}";
}

public record AmazonPartLink(string Url) : PartLink
{
	/// <inheritdoc />
	public override string GetUrl() => Url;
}

public record RockAutoPartLink(string PartNumber) : PartLink
{
	public override string GetUrl() => $"https://www.rockauto.com/en/moreinfo.php?pk={PartNumber}";
}
