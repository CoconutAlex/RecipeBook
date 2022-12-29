export interface Ingredient {
    id: string;
    ingredientName: IngredientName;
    ingredientQuantity: IngredientQuantity;
}

export interface IngredientName {
    id: string;
    name: string;
}

export interface IngredientQuantity {
    id: string;
    quantity: string;
}