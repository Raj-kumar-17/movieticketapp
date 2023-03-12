import { useState,useEffect } from "react";
import axios from "axios";
import { json, Link } from "react-router-dom";
import { Button, Modal, ModalBody, ModalHeader, Form, FormGroup, Label, Input} from 'reactstrap';
import jwtDecode from "jwt-decode";
import styled from "styled-components";
import Pagination from "./Pagination";
import Loader from "./Loader";

/*--------------Styled-Components--------------------*/
const Cards=styled.div`
max-width: 2000px;
margin: 0 auto;
display:grid;
grid-gap: 20px;
align-items: stretch;
padding-top:100px;
padding-bottom:100px;

@media (min-width: 700px) {
  grid-template-columns: repeat(2, 1fr);

  }
  
  @media (min-width: 1100px) {
    grid-template-columns: repeat(3, 1fr); 
  }

  @media (min-width: 1500px) {
    grid-template-columns: repeat(4, 1fr); 
  }
`;

const Card=styled.div`
background-color: dodgerblue;
color: white;
margin-left:35px;
width:300px;
height:400px;
display:flex;
align-item:center;
justify-content:center;
`;

const CardContainer=styled.div`
background:linear-gradient(0deg, rgba(0, 0, 0, 0.50), rgba(0, 0, 0, 0.50)),url('https://imgs.search.brave.com/NS6SDh8BSo6N5uQmMZnZ4EPA8LTbk_xGSkdOX2hZ7bU/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly93d3cu/d2FsbHBhcGVydXAu/Y29tL3VwbG9hZHMv/d2FsbHBhcGVycy8y/MDE1LzEyLzEyLzg1/OTE2NS83ODZjM2Ri/Yzk1Yzg3MTM1ZTE2/NDUxZDIwMTNjYjZj/NS5qcGc');
background-repeat: no-repeat;
background-attachment: fixed;
background-size: cover;
min-height:100vh;
overflow:hidden;
margin-bottom:0;
`;

const Title=styled(Button)`
position:absolute;
height:40px;
margin-top:330px;
font-weight:500;
font-size:18px;
background:red;
border:none;
`;

const ADButton=styled(Button)`
height:35px;
margin:10px;
font-size:15px;
background:red;
border:none;
`;

const ADAddButton=styled(Button)`
height:35px;
margin:10px;
font-size:15px;
font-weight:600;
background:red;
border:none;
display:inline-block;
float:right;
`;

const ADLogoutButton=styled(Button)`
height:35px;
margin:10px;
font-size:15px;
font-weight:600;
background:red;
border:none;
`;

const Image=styled.img`
width:300px;
height:400px;
`;

const SpanKey=styled.span`
font-weight:bold;
font-size:18px;
`;

const SpanValue=styled.span`
font-size:18px;
`;

const Row=styled.div`
display:flex;
align-item:center;
justify-content:center;
position:absolute;

`;
const AdminButton=styled(Link)`
font-size:15px;
 text-decoration: none;
 color:#fff;
 font-weight:600;
 cursor:pointer;
margin:10px;
display:inline-block;
background:red;
padding:8px;
border-radius:5px;
`;

/*-----------------------------------*/

const UserPage=()=>{
    const [open, setOpen] = useState(false);
    const [Addopen, setAddOpen] = useState(false);
    const [Updateopen, setUpdateOpen] = useState(false);
    const [data, setdata] = useState([]);
    const [modal, setmodal] = useState([]);
    const [adddata, setadddata] = useState({});
    const [currentPage,setCurrentPage]=useState(1);
    const[postPerPage]=useState(12);
    const[loading,setLoading]=useState(true);
    const [updatedata, setupdatedata] = useState({"name":"","duration":"","genre":"","rating":"","imageUrl":"","ticketCount":"","movie_id":""});
    const[authenticated,setauthenticated]=useState(false);

    const GetAllMovies=async()=>{
      await axios.get('http://localhost:44421/api/movies/getallmovies')
      .then(res=>res.data)
      .then(val=>{setdata(val);setLoading(false)})
      .catch(err=>console.log(err))  
    }

    const checkisvalidtoken=()=>{
      let token = localStorage.getItem("JWT");
      if(token){
        let decodedToken = jwtDecode(token);
        let currentDate = new Date();  
        if (decodedToken.exp * 1000 < currentDate.getTime()) {
          setauthenticated(false); 
          return false;
        } else {
          setauthenticated(true);  
          return true;
        }
      }
    
    
    }
    const currentModel=()=>{
      return Object.keys(modal).map((key)=>(
      key!="id"&&key!="imageUrl"?<li> <SpanKey>{key}: </SpanKey> <SpanValue>{modal[key]}</SpanValue></li>:''))
      }

      const  AddMovie=async()=>{
        await axios({
          method: "post",
          url: "http://localhost:44421/api/useraction/addmovie",
          data: adddata,
          headers: { "Content-Type": "application/json" ,
                     "Authorization":`Bearer ${localStorage.getItem("JWT")}`
        },
        })
          .then(function (response) {
           console.log(response)
           alert("Movie Added Successfully");
           setAddOpen(!Addopen)
           window.location.reload(false);
          })
          .catch(function (response) {
              if(response.status===403)
                alert("You Cannot Insert Two Movies Twice");

              if(response.status===500)
                alert("Failed to Insert");
            console.log(response);
          });

      }

      const UpdateMovie=async()=>{

        await axios({
          method: "put",
          url: `http://localhost:44421/api/useraction/updatemovie`,
          data:updatedata,
          headers: { "Content-Type": "application/json" ,
                     "Authorization":`Bearer ${localStorage.getItem("JWT")}`
        },
        })
          .then(function (response) {
           alert("Movie Updated Successfully");
           setUpdateOpen(!Updateopen)
           window.location.reload(false);
          })
          .catch(function (response) {
            alert("failed to Update Movie");
            console.log(response);
          });

      }

      const DeleteMovie=async(id)=>{
        if(window.confirm("Are you sure want to delete")==true){
               
        }else{
          return;
        }
        await axios({
          method: "delete",
          url: `http://localhost:44421/api/useraction/deletemovie/${id}`,
          headers: { "Content-Type": "application/json" ,
                     "Authorization":`Bearer ${localStorage.getItem("JWT")}`
        },
        })
          .then(function (response) {
           alert("Movie Deleted Successfully");
           setAddOpen(!Addopen)
           window.location.reload(false);
          })
          .catch(function (response) {
            alert("failed to Delete Movie");
            console.log(response);
          });


      }

  const handleAddChange=(key, value)=>{
    setadddata({...adddata, [key] : value})
    console.log(adddata);
  }

  const handleUpdateChange=(e)=>{
    setupdatedata(updatedata=>({...updatedata, [e.target.name] : e.target.value}))

    console.log(updatedata);

  }

const logoutAdmin=()=>{
setauthenticated(false);
localStorage.clear();
}

    useEffect(() => {
       GetAllMovies();
       checkisvalidtoken();    
    },[]);

const lastMovie=currentPage*postPerPage;
const firstMovie=lastMovie-postPerPage;
const currentPost=data.slice(firstMovie,lastMovie);
const paginate = pageNumber => setCurrentPage(pageNumber);

console.log(updatedata)

return(
<CardContainer>

        <AdminButton to="/AdminLoginPage" data-testId="adminbutton">{authenticated?"Logged in as Admin":"Login as Admin"}</AdminButton>
        {authenticated&&<ADAddButton onClick={()=>setAddOpen(true)}>Add Movie</ADAddButton>}
        {authenticated&&<ADLogoutButton onClick={()=>logoutAdmin()}>Logout</ADLogoutButton>}
<Modal data-testId="Description-Modal" isOpen={open} toggle={() => setOpen(!open)}>
  <ModalHeader toggle={() => setOpen(!open)}>
    Movie Details
  </ModalHeader>
  <ModalBody>
    {currentModel()}
  </ModalBody>
        </Modal>

        <Modal isOpen={Addopen} toggle={() => setAddOpen(!Addopen)}>
  <ModalHeader toggle={() => setAddOpen(!Addopen)}>
    Add movie
  </ModalHeader>
  <ModalBody>
  <Form>
        <FormGroup>
          <Label for="Name">Name</Label>
          <Input type="text" name="Name" onChange={(e)=>handleAddChange('Name',e.target.value)}  />
        </FormGroup>
        <FormGroup>
          <Label for="Duration">Duration</Label>
          <Input type="text" name="Duration" onChange={(e)=>handleAddChange('Duration',e.target.value)} />
        </FormGroup>
        <FormGroup>
          <Label for="Genre">Genre</Label>
          <Input type="text" name="Genre" onChange={(e)=>handleAddChange('Genre',e.target.value)}  />
        </FormGroup>
        <FormGroup>
          <Label for="Rating">Rating</Label>
          <Input type="text" name="Rating" onChange={(e)=>handleAddChange('Rating',e.target.value)}  />
        </FormGroup>
        <FormGroup>
          <Label for="imageUrl">Image Url</Label>
          <Input type="text" name="ImageUrl" onChange={(e)=>handleAddChange('imageUrl',e.target.value)}  />
        </FormGroup>
        <FormGroup>
          <Label for="Ticket Count">Ticket count</Label>
          <Input type="text" name="TicketCount" onChange={(e)=>handleAddChange('TicketCount',e.target.value)} />
        </FormGroup>
        <FormGroup>
          <Label for="Movie Id">Movie Id</Label>
          <Input type="text" name="movie_id" onChange={(e)=>handleAddChange('movie_id',e.target.value)}  />
        </FormGroup>

        <Button onClick={()=>AddMovie()}>Add</Button>
        </Form>
  </ModalBody>
        </Modal>

        <Modal isOpen={Updateopen} toggle={() => setUpdateOpen(!Updateopen)}>
  <ModalHeader toggle={() => setUpdateOpen(!Updateopen)}>
    Update Movie
  </ModalHeader>
  <ModalBody>
  <Form>
        <FormGroup>
          <Label for="Name">Name</Label>
          <Input type="text" name="name" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.name}  />
        </FormGroup>
        <FormGroup>
          <Label for="Duration">Duration</Label>
          <Input type="text" name="duration" onChange={(e)=>handleUpdateChange(e)}  defaultValue={updatedata.duration} />
        </FormGroup>
        <FormGroup>
          <Label for="Genre">Genre</Label>
          <Input type="text" name="genre" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.genre}  />
        </FormGroup>
        <FormGroup>
          <Label for="Rating">Rating</Label>
          <Input type="text" name="rating" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.rating}  />
        </FormGroup>
        <FormGroup>
          <Label for="imageUrl">Image Url</Label>
          <Input type="text" name="imageUrl" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.imageUrl} />
        </FormGroup>
        <FormGroup>
          <Label for="Ticket Count">Ticket count</Label>
          <Input type="text" name="ticketCount" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.ticketCount} />
        </FormGroup>
        <FormGroup>
          <Label for="Movie Id">Movie Id</Label>
          <Input type="text" name="movie_id" onChange={(e)=>handleUpdateChange(e)} defaultValue={updatedata.movie_id}/>
        </FormGroup>

        <Button onClick={()=>UpdateMovie()}>Update</Button>
        </Form>
  </ModalBody>
 
        </Modal>



{loading?<Loader data-testid="loader"/>:<Cards data-testid="resolved">
  {currentPost.map((key)=>
  <Card><Image src={key.imageUrl} alt=""/><Title data-testId="btn" onClick={()=>{setOpen(true);setmodal(key)}}>{key.name}</Title><br/> {authenticated&&<Row><ADButton onClick={()=>{setUpdateOpen(true);setupdatedata(key)}}>Update</ADButton><ADButton onClick={()=>DeleteMovie(key.movie_id)}>Delete</ADButton></Row>}</Card>
)}
</Cards>}
<Pagination data-testId="pagination"
        postsPerPage={postPerPage}
        totalPosts={data.length}
        paginate={paginate}/>
  
</CardContainer>
);
}

export default UserPage;
