import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/models/admin/user.service';
import { Observable } from 'rxjs';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { Category } from 'src/app/models/database/notes/category';

@Component({
    selector: 'note-items-app',
    templateUrl: './note-items.component.html',
    providers: [ NoteItemService, UserService ]
})
export class NoteItemsComponent extends NoteModelsComponent<NoteItem> {
    constructor(router: Router, userService: UserService, noteItemService: NoteItemService,route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, userService, noteItemService, route, snackBar);
    }

    categories: Category[] = [];

    @ViewChild('title') 
    titleModel: NgModel;

    @ViewChild('description') 
    descriptionModel: NgModel;

    @ViewChild('category') 
    categoryModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.titleModel?.invalid || this.descriptionModel?.invalid || this.categoryModel?.invalid);
    }

    protected override getUserModels(attribute?: string | undefined, orderBy?: string | undefined, pageNumber?: number | undefined, pageSize?: number | undefined): Observable<IndexViewModel<NoteItem>> {
        return this.userService.getUserNoteItems(attribute, orderBy, pageNumber, pageSize);
    }

    override modelPreparationBeforeSaving() {
        super.modelPreparationBeforeSaving();
        this.editedModel!.category = null;
    }

    override ngOnInit(): void {
        super.ngOnInit();

        this.userService.getUserCategories().subscribe((indexViewCategories: IndexViewModel<Category>|null) => {
            if(indexViewCategories)
                this.categories = indexViewCategories.models;
        });
    }
}
