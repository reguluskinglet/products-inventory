import axios from 'axios';

const MyAxiosFetch = axios.create();

MyAxiosFetch.interceptors.response.use((response: { data: any; }) => {
    return response.data;
}, (error: any) => {
    return Promise.reject(error);
});
export default MyAxiosFetch;
