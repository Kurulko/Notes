import { MatSnackBar, MatSnackBarConfig  } from '@angular/material/snack-bar';
import { OperatorFunction, of } from 'rxjs';
import { BaseComponent } from './base.component';
import { Component, Input } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'edit-model-app',
    templateUrl: './edit-model.component.html',
})
export class EditModelComponent extends BaseComponent{
    @Input() 
    errors: string[] | null = null;

    constructor(private snackBar: MatSnackBar){
        super();
    }

    protected catchError<T>() : OperatorFunction<T, T>{
        return catchError((error: any) => {
            this.handleError(error);
            return [];
        })
    }

    protected handleError(errors: string[]) {
        this.errors = errors;
        this.errorOccured(errors.join(';\n'))
        return of(errors);
    }

    protected errorOccured(error:string){
        this.showSnackbar(`ERROR: ${error}`)
    }

    protected modelUpdatedSuccessfully(modelName?:string){
        this.operationDoneSuccessfully('updated', modelName);
    }

    protected modelAddedSuccessfully(modelName?:string){
        this.operationDoneSuccessfully('added', modelName);
    }

    protected modelDeletedSuccessfully(modelName?:string){
        this.operationDoneSuccessfully('deleted', modelName);
    }

    private operationDoneSuccessfully(actionName:string, modelName:string  = 'Model'){
        this.showSnackbar(`${modelName} ${actionName} successfully`)
    }

    protected showSnackbar(message: string): void {
        const config = new MatSnackBarConfig();
        config.duration = 2500;
        config.verticalPosition = 'top';

        this.snackBar.open(message, 'Close', config);
    }
}