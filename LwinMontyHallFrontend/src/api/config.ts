import { AxiosRequestConfig } from 'axios';

export const axiosRequestConfig: AxiosRequestConfig = {
    baseURL: 'http://localhost:5500/',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json',
        'x-api-version': '1'
    }
}