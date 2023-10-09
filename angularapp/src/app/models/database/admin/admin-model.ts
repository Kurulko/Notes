import { DbModel } from "../db-model";
import { v4 } from 'uuid';

export class AdminModel implements DbModel { 
    id:string = v4();
}