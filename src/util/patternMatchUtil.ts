export const exhaustivePatternMatch = (input: never) => {
	throw new Error(`Unhandled case: ${JSON.stringify(input)}`)
};
