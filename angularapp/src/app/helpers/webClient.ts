import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export class WebClient {
    constructor(private httpClient: HttpClient, private pathBase: string, private headers: {headers: HttpHeaders}) {}

    // post(url: string, body: any): Observable<Object>;
    // post<T>(url: string, body: any): Observable<T>;

    // post<T>(url: string, body: any): T extends Object ? Observable<T> : Observable<Object> {
    //     if (typeof body === 'object') {
    //         return this.httpClient.post<T>(`${this.pathBase}/${url}`, body, this.headers);
    //     } else {
    //         return this.httpClient.post(`${this.pathBase}/${url}`, body, this.headers);
    //     }
    // }
    // post<T>(url: string, body: any): T extends object ? Observable<T> : Observable<object> {
    //     if (typeof body === 'object') {
    //         return this.httpClient.post<T>(`${this.pathBase}/${url}`, body, this.headers) as T extends object ? Observable<T> : Observable<object>;
    //     } else {
    //         return this.httpClient.post(`${this.pathBase}/${url}`, body, this.headers) as T extends object ? Observable<T> : Observable<object>;
    //     }
    // }
    
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