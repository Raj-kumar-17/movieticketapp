import '@testing-library/jest-dom';
import { render, cleanup,waitFor,fireEvent,screen, getByRole} from "@testing-library/react";
import "@testing-library/jest-dom";
import axiosMock from "axios";
import UserPage from '../Pages/UserPage';
import { BrowserRouter } from 'react-router-dom';

afterEach(cleanup);
const datas=[ {"_id":"64072333778ceb42401ddd5f","Name":"Inception","Duration":"2h","Genre":"Action,Thriller","Rating":"9.4","ImageUrl":"https://m.media-amazon.com/images/M/MV5BZGFjOTRiYjgtYjEzMS00ZjQ2LTkzY2YtOGQ0NDI2NTVjOGFmXkEyXkFqcGdeQXVyNDQ5MDYzMTk@._V1_.jpg","TicketCount":"100","movie_id":"18"}];


//Axios get
it("fetches and displays data", async () => {
	axiosMock.get.mockResolvedValueOnce({data:datas});  
	const url = "http://localhost:44421/api/movies/getallmovies";
    const {getByTestId}=render(<BrowserRouter><UserPage /></BrowserRouter>)
	await waitFor(() =>{expect(getByTestId('resolved')).toBeInTheDocument()});

	expect(axiosMock.get).toHaveBeenCalledTimes(1);
	expect(axiosMock.get).toHaveBeenCalledWith(url);
  });



//checking button context for normal User
  test("to Check Initially the user is Normal User", async()=>{
    axiosMock.get.mockResolvedValueOnce({data:{}});  
    const {getByTestId}=render(<BrowserRouter><UserPage /></BrowserRouter>)
	await waitFor(() =>{expect(getByTestId('adminbutton')).toHaveTextContent('Login as Admin')});
    })

	//checking button context for not an admin
	test("to Check Initially the user is admin", async()=>{
		axiosMock.get.mockResolvedValueOnce({data:datas});  
		const {getByTestId}=render(<BrowserRouter><UserPage /></BrowserRouter>)
		await waitFor(() =>{expect(getByTestId('adminbutton')).not.toHaveTextContent('Logged in as Admin')});
		})

	//Is Modal Popped
	test("Is modal popped",async() => {
	
	    axiosMock.get.mockResolvedValueOnce({data:datas});
		const url = "http://localhost:44421/api/movies/getallmovies";  
    const {getByTestId}=render(<BrowserRouter><UserPage /></BrowserRouter>)
	await waitFor(() =>{expect(getByTestId('resolved')).toBeInTheDocument()});
	fireEvent.click(screen.getByTestId('btn'))
	await waitFor(() =>{expect(getByTestId('Description-Modal')).toHaveTextContent('Movie Details')});

	expect(axiosMock.get).toHaveBeenCalledTimes(1);
	expect(axiosMock.get).toHaveBeenCalledWith(url);
	})
	  




