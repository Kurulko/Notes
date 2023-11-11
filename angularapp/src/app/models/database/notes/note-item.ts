import { Category } from "./category";
import { NoteModel } from "./note-model";

export class NoteItem extends NoteModel {
    title: string;
    description: string;

    categoryId: number;
    category: Category|null = new Category();
}