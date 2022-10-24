import initializeAxios from './axios';
import { axiosRequestConfig } from './config';
import { Observable, defer, map } from 'rxjs';

const axiosInstance = initializeAxios(axiosRequestConfig);

const get = <T>(url: string, queryParams?: object): Observable<T> => {
    return defer(()=> axiosInstance.get<T>(url, { params: queryParams }))
        .pipe(map(result => result.data));
};

const post = <T>(url: string, body: object, queryParams?: object): Observable<T> => {
    return defer(()=> axiosInstance.post<T>(url, body, { params: queryParams }))
        .pipe(map(result => result.data));
};

const put = <T>(url: string, body: object, queryParams?: object): Observable<T | void> => {
    return defer(()=>axiosInstance.put<T>(url, body, { params: queryParams }))
        .pipe(map(result => result.data));
};

const patch = <T>(url: string, body: object, queryParams?: object): Observable<T | void> => {
    return defer(()=> axiosInstance.patch<T>(url, body, { params: queryParams }))
        .pipe(map(result => result.data));
};

const deleteR = <T>(url: string, id:number): Observable<T | void> => {
    return defer(() => (axiosInstance.delete(`${url}/${id}` )))
        .pipe(map(result => result.data)
    );
};

const APIs = { get, post, put, patch, deleteR };

export default APIs;
