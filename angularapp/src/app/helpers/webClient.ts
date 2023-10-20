import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenViewModel } from '../models/auth/token-viewmodel';

export class WebClient {
    constructor(private httpClient: HttpClient, private pathBase: string, private headers: {headers: HttpHeaders}) {}

    get(url: string): Observable<Object>;
    get<T>(url: string): Observable<T>;
    
    get<T>(url:string): Observable<T> {
        return this.httpClient.get<T>(`${this.pathBase}/${url}`, this.headers);
    }

    delete(url:string): Observable<Object> {
        return this.httpClient.delete(`${this.pathBase}/${url}`, this.headers);
    }

    post(url: string, body?: any): Observable<Object>;
    post<T>(url: string, body?: any): Observable<T>;

    post<T>(url: string, body?: any): Observable<T> {
        return this.httpClient.post<T>(`${this.pathBase}/${url}`, body, this.headers);
    }

    patch(url: string, body?: any): Observable<Object>;
    patch<T>(url: string, body?: any): Observable<T>;

    patch<T>(url: string, body?: any): Observable<T> {
        return this.httpClient.patch<T>(`${this.pathBase}/${url}`, body, this.headers);
    }

    put(url: string, body?: any): Observable<Object>;
    put<T>(url: string, body?: any): Observable<T>;

    put<T>(url: string, body?: any): Observable<T> {
        return this.httpClient.put<T>(`${this.pathBase}/${url}`, body, this.headers);
    }
}