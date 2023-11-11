import { NoteItem } from "./note-item";
import { NoteModel } from "./note-model";

export class Category extends NoteModel {
    name: string;

    noteItems: NoteItem[]|null;
}