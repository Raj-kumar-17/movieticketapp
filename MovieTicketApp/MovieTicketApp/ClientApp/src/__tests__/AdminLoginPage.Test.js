import '@testing-library/jest-dom';
import { render,waitFor,screen,fireEvent} from "@testing-library/react";
import userEvent from '@testing-library/user-event';
import "@testing-library/jest-dom";
import axiosMock from "axios";
import AdminLoginPage from '../Pages/AdminLoginPage';
import { BrowserRouter } from 'react-router-dom';


const datas=[ {"Name":"The ShawShank Redemption","Duration":"2h","Genre":"Thriller","Rating":"9.2","TicketCount":"120","movie_id":"19"}];
test('check for valid form in the UI',async()=>{
const {getByTestId}=render(<BrowserRouter><AdminLoginPage /></BrowserRouter>)
await waitFor(() =>{expect(getByTestId('username')).toBeInTheDocument();expect(getByTestId('username')).toHaveAttribute("type", "text");});
await waitFor(() =>{expect(getByTestId('password')).toBeInTheDocument();expect(getByTestId('password')).toHaveAttribute("type", "password");});

})

test('check for valid form sent to backend',async()=>{
    const {getByTestId}=render(<BrowserRouter><AdminLoginPage /></BrowserRouter>)
    
    await waitFor(() =>{expect(getByTestId('username')).toBeInTheDocument()});
    userEvent.type(screen.getByTestId("username"),"raj")
    await waitFor(() =>expect(screen.getByTestId("username")).toHaveValue("raj"));
    await waitFor(() =>expect(screen.queryByTestId("invaliduser")).not.toBeInTheDocument());

    await waitFor(() =>{expect(getByTestId('password')).toBeInTheDocument()});
    userEvent.type(screen.getByTestId("password"),"admin")
    await waitFor(() =>expect(screen.getByTestId("password")).toHaveValue("admin"));
    await waitFor(() =>expect(screen.queryByTestId("invalidpassword")).not.toBeInTheDocument());
    

    })

    test('check for invalid data before sending form to backend',async()=>{
        const url = "http://localhost:44421/api/movies/getallmovies";
        const {getByTestId}=render(<BrowserRouter><AdminLoginPage /></BrowserRouter>)
        
        await waitFor(() =>{expect(getByTestId('username')).toBeInTheDocument()});
        await waitFor(() =>{expect(getByTestId('submit-btn')).toBeInTheDocument()});
        axiosMock.post.mockResolvedValueOnce({data:datas});  
        fireEvent.click(screen.getByTestId('submit-btn'))
        await waitFor(() =>expect(screen.queryByTestId("invaliduser")).toBeInTheDocument());
    
        await waitFor(() =>{expect(getByTestId('password')).toBeInTheDocument()});
        await waitFor(() =>{expect(getByTestId('submit-btn')).toBeInTheDocument()});
        axiosMock.post.mockResolvedValueOnce({data:datas}); 
        fireEvent.click(screen.getByTestId('submit-btn'))
        await waitFor(() =>expect(screen.queryByTestId("invalidpassword")).toBeInTheDocument());
    
    })

/*test('check for Authorization error pop up', async () => {
    const url = "http://localhost:44421/api/movies/getallmovies";
    const { getByTestId } = render(<BrowserRouter><AdminLoginPage /></BrowserRouter>)
    await waitFor(() => { expect(getByTestId('username')).toBeInTheDocument() });
    await waitFor(() => { expect(getByTestId('password')).toBeInTheDocument() });
    await waitFor(() => { expect(getByTestId('submit-btn')).toBeInTheDocument() });
    axiosMock.post.mockResolvedValueOnce({data:datas}); 
    userEvent.type(screen.getByTestId("username"), "raj")
    userEvent.type(screen.getByTestId("password"), "Admin@123")
    fireEvent.click(screen.getByTestId('submit-btn'))
    await waitFor(() => {expect(getByTestId("unauth-modal")).not.toBeInTheDocument()});
    expect(axiosMock.get).toHaveBeenCalledTimes(1);
	expect(axiosMock.get).toHaveBeenCalledWith(url);

})
*/
