import { Component, OnInit, TemplateRef, ViewChild, Input, ContentChild, OnDestroy, Output, EventEmitter  } from '@angular/core';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { EditModelComponent } from '../helpers/edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { UnaryFunction } from 'rxjs';
import { Observable } from 'rxjs'
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { OrderBy } from 'src/app/helpers/orderBy';

@Component({
    selector: 'models-app',
    templateUrl: "./models.component.html",
})
export class ModelsComponent<T extends DbModel, K extends string|number> extends EditModelComponent implements OnInit, OnDestroy  {
    @Input() 
    pageSize: number = 5;

    @Input() 
    getModels: (attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number) => Observable<IndexViewModel<T>>;

    @Input() 
    createModel: UnaryFunction<T, Observable<Object>>;

    @Input() 
    updateModel: UnaryFunction<T, Observable<Object>>;

    @Input() 
    removeModel: UnaryFunction<K, Observable<Object>>;

    @Input() 
    isSaveModel: () => boolean;

    @ViewChild('readOnlyTemplate', {static: false})
    readOnlyTemplate: TemplateRef<any>|null;

    @ViewChild('editTemplate', {static: false})
    editTemplate: TemplateRef<any>|null;

    @ContentChild('readOnlyTemplate') 
    readOnlyTemplateChild: TemplateRef<any>;

    @ContentChild('editTemplate')
    editTemplateChild: TemplateRef<any>;

    @ContentChild('tableHeader')
    tableHeaderChild: TemplateRef<any>;

    @Input() 
    indexViewModel: IndexViewModel<T>|null = null;
    @Output() 
    indexViewModelChange = new EventEmitter<IndexViewModel<T>>();

    editedModel: T|null = null;
    isNewRecord: boolean = false;

    pageNumber:number = 1;

    @Input() 
    attribute:string = 'id';
    @Output() 
    attributeChange = new EventEmitter<string>();

    @Input() 
    orderBy:string = OrderBy.ASC;
    @Output() 
    orderByChange = new EventEmitter<string>();

    get sortDirectionStr(): string {
        return this.orderBy === OrderBy.ASC ? '↑' : '↓';
    }

    isSort(property:string): boolean {
        return this.attribute === property;
    }

    get isEditedModel(): boolean {
        return this.editedModel != null;
    }
      
    get isEmpty(): boolean {
        return this.indexViewModel?.models?.length === 0 ?? true;
    }

    get isLoading(): boolean {
        return this.indexViewModel === null || this.indexViewModel === undefined;
    }

    get isOverOneElement(): boolean {
        return (this.indexViewModel?.models?.length ?? 0) > 1;
    }

    get getCSSClass(): string {
        return this.isOverOneElement ? 'custom-cursor' : '';
    }

    private routeSubscription: Subscription;
    constructor(protected router: Router, protected route: ActivatedRoute, snackBar: MatSnackBar){
        super(snackBar);
    }

    sortArray(fieldName: string) {
        if(this.isOverOneElement)
        {
            if (this.attribute === fieldName) {
                this.changeOrderBy();
            } else {
                this.attribute = fieldName;
                this.attributeChange.emit(fieldName);
                this.orderBy = OrderBy.ASC;
            }

            this.router.navigate(
                [], 
                {
                    queryParams:{
                        'page': this.pageNumber, 
                        'attribute': this.attribute,
                        'orderBy': this.orderBy
                    }
                }
            );
            
            this.orderByChange.emit(this.orderBy);
            this.loadModels();
        }
    }

    private changeOrderBy(){
        if(this.orderBy ===  OrderBy.ASC){
            this.orderBy = OrderBy.DESC;
        }
        else {
            this.orderBy = OrderBy.ASC;
        }
    }

    ngOnInit(): void {
       this.loadModels();

       this.routeSubscription = this.route.queryParams.subscribe(
            params => {
                const pageNumber = parseInt(params['page']);
                if(pageNumber && pageNumber > 0)
                    this.pageNumber =  pageNumber;

                const attribute = params['attribute'];
                if(attribute)
                    this.attribute =  attribute;
                
                const orderBy = params['orderBy'];
                if(orderBy)
                    this.orderBy =  orderBy;

                this.loadModels();
            }
        );
    }

    private loadModels(){
        this.getModels(this.attribute, this.orderBy, this.pageNumber, this.pageSize)
            .pipe(this.catchError())
            .subscribe((indexViewModel: IndexViewModel<T>) => {
                this.indexViewModel = indexViewModel;
                this.indexViewModelChange.emit(indexViewModel);
            })
    }

    private createEmptyModel(): T{
        const emptyModel = {} as T;
        return emptyModel;
    }

    addModel(){
        this.editedModel = this.createEmptyModel();
        this.indexViewModel!.models.push(this.editedModel!);
        this.isNewRecord = true;
    }

    editModel(model: T){
        this.editedModel = {...model};
    }

    loadTemplate(model: T) {
        if(this.editedModel && this.editedModel.id === model.id)
            return this.editTemplate;
        return this.readOnlyTemplate;
    }

    saveModel() {
        (this.isNewRecord ? 
            this.createModel(this.editedModel as T) : 
            this.updateModel(this.editedModel as T))
                .pipe(this.catchError())
                .subscribe(_ => {
                    this.loadModels();
                    this.modelUpdatedSuccessfully()
                });
        if(this.isNewRecord) {
            this.isNewRecord = false;
        }
        this.editedModel = null;
    }
    
    cancel() {
        if(this.isNewRecord){
            this.indexViewModel!.models.pop();
            this.isNewRecord = false;
        }
        this.editedModel = null;
    }

    deleteModel(model: T){
        this.removeModel(model!.id as K)
            .pipe(this.catchError())
            .subscribe(_ => {
                this.loadModels();
                this.modelDeletedSuccessfully()
            });
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
    }

    protected redirectToHome(){
        this.redirectTo('/');
    }

    protected redirectTo(path:string){
        this.router.navigateByUrl(path);
    }
}
