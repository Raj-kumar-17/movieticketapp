import styled from "styled-components";


const Container=styled.div`
height: 100vh;
width: 100%;
display: flex;
align-items: center;
justify-content: center;
`;

const Spinner=styled.div`
margin: 0 10px;
width: 50px;
height: 50px;
border-radius: 50%;
border: 4px solid #f3f3f3;
border-top: 4px solid red;
animation: rotating-spinner 1s linear infinite;

@keyframes rotating-spinner {
    from {
      transform: rotate(0deg);
    }
    
    to {
      transform: rotate(360deg);
    }
  }
`;



const Loader=()=>{
return(
  <Container>
<Spinner>
</Spinner>
  </Container>
);


}

export default Loader;