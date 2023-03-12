import styled from "styled-components";
import { useState } from "react";
import {useNavigate } from "react-router-dom";
import {Modal, ModalBody, ModalHeader} from 'reactstrap';
import axios from "axios";


const Body = styled.body`
display: flex;
justify-content: center;
align-items: center;
width: 100%;
height: 100vh;
font-family: 'Roboto', sans-serif;
background-color: rgb(229, 231, 235);
`;

const Heading = styled.h2`
display: flex;
justify-content: center;
align-items: center;
font-size: 1.5rem;
font-weight: 900;
color: rgb(55, 65, 81);
`;

const Form = styled.form`
flex-direction: column;
justify-content: flex-start;
align-items: flex-start;
padding: 48px;
margin-top: 2rem;
width: 370px;
height: 400px;
background-color: #fff;
border-radius: 4px;
box-shadow: 4px 4px 7px lightgray;
`;

const Label = styled.label`
 display: flex;
	text-transform: capitalize;
	margin-top: 1.2rem;
    margin-bottom: 0.3rem;
	font-size: 0.7rem;
    letter-spacing: 0.5px;
    font-weight:400;
	color: rgb(55, 65, 81);
`;

const Input = styled.input`
	width: 100%;
	line-height: 3rem;
	background-color: rgb(229, 231, 235);
	border: none;
	border-radius: 4px;
`;

const Para = styled.p`
	color:red;
`;

const Button = styled.button`
margin-top: 2.2rem;
	width: 100%;
	height: 3rem;
	text-transform: capitalize;
	font-size: 0.9rem;
	font-weight: 700;
	font-family: 'Roboto', sans-serif;
	color: rgb(220, 229, 247);
	background-color: rgb(37, 99, 235);
	border-radius: 4px;
	border: none;
    cursor: pointer;
`;



const AdminLoginPage =() => {

const navigate=useNavigate();
const [username,setusername]=useState('');
const [password,setpassword]=useState('');
const[open,setOpen]=useState(false);
const[notValidusername,setNotValidusername]=useState(false);
const[notValidpassword,setNotValidpassword]=useState(false);

const authorize=async(e)=>{
	e.preventDefault();
	axios.post("http://localhost:44421/api/useraction/authenticate",{
		UserName: username,
		PassWord: password
	}).then(async function(res)
	{
		console.log(res);
		if(res!=null)
		  localStorage.setItem("JWT",res.data)
		  navigate("/userpage");

	}).catch(err=>{console.log(err);setOpen(true)});

}

const checkValidForm=()=>{

if(username==='')
setNotValidusername(true);
else
setNotValidusername(false);

if(password==='')
setNotValidpassword(true);
else
setNotValidpassword(false);

}
    return (
        <Body>
			<Modal data-testId="Unauth-Modal" isOpen={open} toggle={() => setOpen(!open)}>
  <ModalHeader toggle={() => setOpen(!open)}>
    Authorization Error
  </ModalHeader>
  <ModalBody>
    Invalid username or password<br/>
  </ModalBody>
        </Modal>
            <section>
                <Heading class="title">Log-in</Heading>
                <Form action="login-box">
                    <Label for="username">username</Label>
                    <Input data-testId="username" type="text" onChange={(e)=>setusername(e.target.value)}/>
					{notValidusername&&<Para data-testId="invaliduser">Please enter your Username</Para>}
                        <Label for="password">password</Label>
                        <Input data-testId="password" type="password" onChange={(e)=>setpassword(e.target.value)}/>
						{notValidpassword&&<Para data-testId="invalidpassword">Please enter your Password</Para>}
                            <Button data-testId="submit-btn" onClick={(e)=>{checkValidForm();authorize(e)}}>login</Button>
                </Form>
    </section>



</Body>

)

}

export default AdminLoginPage
