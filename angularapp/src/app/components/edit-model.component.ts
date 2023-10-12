import { MatSnackBar, MatSnackBarConfig  } from '@angular/material/snack-bar';
import { of } from 'rxjs';

export abstract class EditModelComponent{
    constructor(private snackBar: MatSnackBar){
    }

    protected handleError(error:string){
        console.error('Error:', error);
        this.errorOccured(error)
        return of(error);
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