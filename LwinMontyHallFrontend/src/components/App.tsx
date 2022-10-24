import { catchError, Observable, of, Subject, take, throwError } from 'rxjs';
import api from '../api/api';
import { MontyHall } from '../models/montyhall';
import MontyHallProblem from './MontyHallProblem';

export const useObservable = () => {
  const subj = new Subject<boolean>();

  const next = (value:boolean): void => {
      subj.next(value) };

  return { change: subj.asObservable() , next};
};

function App() {

 const createSimulation = <T,>(url: string, item: (MontyHall)): Observable<T> => {
    return api.post<T>(url, item)
        .pipe(
            take(1),            
            catchError(err => {
                console.log(err);
                throw err;
            })
    ) as Observable<T>;
 };

 const readSimulation = <T,> (url: string): Observable<T> => {
  return api.get<T>(url)
       .pipe(
           take(1),
           catchError(err => {
            console.log(err);
            throw err;
        })
       ) as Observable<T>;
};

const updateSimulation = (url :string, item:(MontyHall)) => {
  api.put(url, item)
      .pipe(take(1))
      .subscribe(() => {
      });
};

const deleteSimulation = (url: string, id: number): void => {
  api.deleteR(url, id)
      .subscribe(() => {
      });
};

  return (
    <>
    <div className="ui container">
        <div className="ui internally celled grid">
            <div className="row">
                <div className="sixteen wide column">
                    <MontyHallProblem  create={createSimulation} read={readSimulation} update={updateSimulation} delete={deleteSimulation}/>
                </div>
            </div>
        </div>
    </div>
</>
  );
}

export default App;
