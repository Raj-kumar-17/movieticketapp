import styled from "styled-components";
import { Link } from "react-router-dom";
import bgVideo from "../Media/Videos/CN.mp4"


//Styled Components
const FullScreenVideo = styled.video`
 position: fixed;
  right: 0; 
  bottom: 0;
  min-width: 100%; 
  min-height:100%;
  width: auto; 
  height: auto;
  z-index: -100;
`;

const VideoHead = styled.h1`
  position: absolute;
  bottom: 1%;
  left:5%;
  width: 94%;
  font-size: 36px;
  letter-spacing: 3px;
  color: #fff;
  font-family: Montserrat, sans-serif;
  text-align: left;
`;

const VideoContainer = styled.div`
  margin: 0;
  padding: 0;
  background: #333;
  background-attachment: fixed;
  background-size: cover;
`;

const GoButtons = styled(Link)`
 font-size:15px;
 text-decoration: none;
 color:#fff;
 font-weight:bold;
 cursor:pointer;
`;
/*********************/


const Home = () => {
    return (
        <VideoContainer>
   <div>
  <VideoHead data-testid="test-Header1">Movie Ticket App<br /> <GoButtons data-testid="test-Header2" to="/userpage">{">>"} Show Movies</GoButtons></VideoHead>
</div>
<FullScreenVideo autoPlay loop muted>
  <source src={bgVideo} type="video/webm"/>
</FullScreenVideo>
  </VideoContainer>
    );
}

export default Home;