module private JdmDepot.Data.YamlDotNetHelper

open YamlDotNet.Core
open YamlDotNet.Serialization

let deserializationBuilder a =
    let r = DeserializerBuilder ()
    r.WithNamingConvention a

let withNamingConvention a (b : DeserializerBuilder) = b.WithNamingConvention(a)

let withTagMapping tagName concreteClass (c : DeserializerBuilder) = c.WithTagMapping(TagName(tagName), concreteClass)

let build (a : DeserializerBuilder) = a.Build()

let deserialize<'a> (yaml : string) (a : IDeserializer) = a.Deserialize<'a>(yaml)
