CREATE TABLE Materials
(
    MaterialId INT PRIMARY KEY,
    ColorMaterial VARCHAR(50),
	DefaultMaterial VARCHAR(50),
	IsFunctional BIT,
	PurchaseDate VARCHAR(50),
	SerialNumber VARCHAR(50),
	HaveAnAlimentation BIT,
	MaterialDocumentation VARCHAR(50)
	TypeId INT NULL REFERENCES MaterialTypes(TypeId);
)
