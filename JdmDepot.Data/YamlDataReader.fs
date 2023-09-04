module JdmDepot.Data.YamlDataReader

open System.Collections.Generic
open System.IO
open System.Reflection
open JdmDepot.Common.DataModels
open JdmDepot.Data.YamlDotNetHelper
open YamlDotNet.Serialization.NamingConventions
open FruityFoundation.FsBase

[<RequireQualifiedAccess>]
module YamlModel =
    type FluidType =
        | Oil = 1
        | Coolant = 2

    type AdvanceAutoPartLink () =
        member val PartNumber = "" with get, set

    type AmazonPartLink () =
        member val Url = "" with get, set

    type RockAutoPartLink () =
        member val PartNumber = "" with get, set

    type FluidMaintenanceItem () =
        member val Name = "" with get, set
        member val FluidType = FluidType.Oil with get, set
        member val CapacityQuarts = 0.0M with get, set
        member val Specification = "" with get, set
        member val PartLinks = Array.empty<obj> with get, set
        member val Notes = "" with get, set

    type AlternativePartNumber () =
        member val Text = "" with get, set

    type FilterMaintenanceItem () =
        member val Name = "" with get, set
        member val ManufacturerPartNumber = "" with get, set
        member val AlternativePartNumbers = Array.empty<AlternativePartNumber> with get, set
        member val PartLinks = Array.empty<obj> with get, set
        member val Notes = "" with get, set

    type BeltMaintenanceItem () =
        member val Name = "" with get, set
        member val ManufacturerPartNumber = "" with get, set
        member val AlternativePartNumbers = Array.empty<AlternativePartNumber> with get, set
        member val Quantity = 0us with get, set
        member val PartLinks = Array.empty<obj> with get, set
        member val Notes = "" with get, set

    type MaintenanceItems () =
        member val CarId = "" with get, set
        member val EngineId = "" with get, set
        member val Fluids = Array.empty<FluidMaintenanceItem> with get, set
        member val Filters = Array.empty<FilterMaintenanceItem> with get, set
        member val Belts = Array.empty<BeltMaintenanceItem> with get, set

    type Supplier () =
        member val Id = "" with get, set
        member val Name = "" with get, set

    type SuppliersFile () =
        member val Suppliers = Array.empty<Supplier> with get, set

let private tryFind (b : 'a) (a : IReadOnlyDictionary<'a, 'b>) : 'b option =
    match a.TryGetValue(b) with
    | true, x -> Some x
    | false, _ -> None

let private getPartLink (input : obj) : PartLink =
    match input with
    | :? YamlModel.AdvanceAutoPartLink as x -> AdvanceAutoPartLink(x.PartNumber)
    | :? YamlModel.AmazonPartLink as x -> AmazonPartLink(x.Url)
    | :? YamlModel.RockAutoPartLink as x -> RockAutoPartLink(x.PartNumber)
    | _ -> failwithf "Unsupported type: %A" input

let private yamlFluidToDomainFluid (input : YamlModel.FluidMaintenanceItem) : MaintenanceItem =
    let getTypeOfFluid = function
        | YamlModel.FluidType.Oil -> FluidType.Oil
        | YamlModel.FluidType.Coolant -> FluidType.Coolant
        | x -> failwithf "Unsupported type: %A" x

    FluidMaintenanceItem(
        input.Name,
        getTypeOfFluid input.FluidType,
        input.CapacityQuarts,
        input.Specification,
        input.PartLinks |> Seq.map getPartLink |> Array.ofSeq,
        input.Notes)

let private yamlFilterToDomainFilter (input : YamlModel.FilterMaintenanceItem) : MaintenanceItem =
    FilterMaintenanceItem(
        input.Name,
        input.ManufacturerPartNumber,
        input.AlternativePartNumbers |> Seq.map (fun x -> x.Text) |> Array.ofSeq,
        input.PartLinks |> Seq.map getPartLink |> Array.ofSeq,
        input.Notes)

let private yamlBeltToDomainBelt (input : YamlModel.BeltMaintenanceItem) : MaintenanceItem =
    BeltMaintenanceItem(
        input.Name,
        input.ManufacturerPartNumber,
        input.AlternativePartNumbers |> Seq.map (fun x -> x.Text) |> Array.ofSeq,
        input.Quantity,
        input.PartLinks |> Seq.map getPartLink |> Array.ofSeq,
        input.Notes)

let private readAllTextFromEmbeddedResource (resource : string) =
    use stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)
    match stream with
    | null -> failwithf "Resource not found: %s" resource
    | _ ->
        use reader = new StreamReader(stream)
        reader.ReadToEnd ()

let parseMaintenanceItemsForCarId (carId : string) (engineId : string) : MaintenanceItem IReadOnlyCollection =
    let yaml =
        sprintf "JdmDepot.Data.Cars.%s.MaintenanceItems-%s.yml" (carId.Replace("-", "_").ToLower()) (engineId.Replace("-", "_").ToLower())
        |> readAllTextFromEmbeddedResource
    let result =
        deserializationBuilder HyphenatedNamingConvention.Instance
        |> withTagMapping "!part-link--advance-auto" typedefof<YamlModel.AdvanceAutoPartLink>
        |> withTagMapping "!part-link--amazon" typedefof<YamlModel.AmazonPartLink>
        |> withTagMapping "!part-link--rock-auto" typedefof<YamlModel.RockAutoPartLink>
        |> build
        |> deserialize<YamlModel.MaintenanceItems> yaml

    seq {
        yield! result.Fluids |> Seq.map yamlFluidToDomainFluid
        yield! result.Filters |> Seq.map yamlFilterToDomainFilter
        yield! result.Belts |> Seq.map yamlBeltToDomainBelt
    }
    |> Array.ofSeq
    |> Array.toReadOnlyCollection

let parseSuppliers () : Supplier IReadOnlyCollection =
    let yaml = readAllTextFromEmbeddedResource "JdmDepot.Data.Parts.Suppliers.yml"
    let pick (f : 'a -> 'b) (a: 'a) : 'b = f(a)

    deserializationBuilder HyphenatedNamingConvention.Instance
    |> build
    |> deserialize<YamlModel.SuppliersFile> yaml
    |> pick (fun x -> x.Suppliers)
    |> Seq.map (fun x -> Supplier(SupplierId(x.Id), x.Name))
    |> Array.ofSeq
    |> Array.toReadOnlyCollection
