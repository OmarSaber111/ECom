export interface IProduct {
    name:         string;
    description:  string;
    newPrice:     number;
    oldPrice:     number;
    categoryName: string;
    photos:       IPhoto[];
}

export interface IPhoto {
    imgName: string;
    id:      number;
}
