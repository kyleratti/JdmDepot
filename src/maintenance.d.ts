export type PartLink =
	| { kind: "amazon"; url: string; }
	| { kind: "advance-auto"; ["part-number"]: string; }
	| { kind: "rockauto"; ["part-number"]: string; }
	| { kind: "napa"; ["part-number"]: string; url: string; }
	| { kind: "fcp-euro"; ["part-number"]: string; url: string; }

export type ReplacementPartCategory =
	| "cooling";

export type MaintenanceItems = {
	["car-id"]: string;
	["engine-id"]: string;
	fluids: {
		name: string;
		["fluid-type"]: "oil" | "coolant";
		["capacity-quarts"]: number;
		specification: string;
		["part-links"]: PartLink[];
		notes: string | undefined;
	}[];
	filters: {
		name: string;
		["manufacturer-part-number"]: string;
		["alternative-part-numbers"]: { text: string; }[] | undefined;
		["part-links"]: PartLink[] | undefined;
		notes: string | undefined;
	}[];
	belts: {
		name: string;
		["manufacturer-part-number"]: string;
		quantity: number;
	}[];
	hoses: {
		name: string;
		["size-mm"]: number;
		["length-inches"]: number;
		quantity: number;
	}[];
	["replacement-parts"]: {
		name: string;
		category: ReplacementPartCategory;
		quantity: number;
		["manufacturer-part-number"]: string;
		["alternative-part-numbers"]: { text: string; }[] | undefined;
		["part-links"]: PartLink[] | undefined;
		notes: string | undefined;
	}[];
};

declare module "maintenance*.yml" {
	const value: MaintenanceItems;
	export default value;
}