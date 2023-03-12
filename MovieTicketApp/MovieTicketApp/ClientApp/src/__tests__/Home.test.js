import '@testing-library/jest-dom';
import {render, screen} from '@testing-library/react';
import Home from '../Pages/Home';
import { MemoryRouter } from "react-router-dom";

test("If the Header 1 is Visible", ()=>{
render(<MemoryRouter> 
    <Home/>
  </MemoryRouter>)
expect(screen.getByTestId(`test-Header1`)).toBeInTheDocument()
})

test("If the Header 2 is Visible", ()=>{
    render(<MemoryRouter> 
    <Home/>
  </MemoryRouter>)
    expect(screen.getByTestId(`test-Header2`)).toBeInTheDocument()
    })