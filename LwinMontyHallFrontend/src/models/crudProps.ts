import {Observable} from "rxjs";

export interface CrudProps<T>  {
    create: (url: string, item: T) => Observable<T>;
    read: (url: string, item: T) => Observable<T>;
    update: (url: string, item: T) => void;
    delete: (url: string, id: number) => void
}
