export interface Recipe {
    id: string;
    title: string;
    description: string;
    steps: number;
    portions: number;
    duration: number;
    difficulty: Difficulty;
    imageName: string;
    ingredients: Ingredient[];
}

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

export enum Difficulty {
    VeryEasy = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3,
    VeryHard = 4
}
