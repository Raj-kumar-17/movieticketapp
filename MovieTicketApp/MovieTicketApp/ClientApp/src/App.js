import Home from "./Pages/Home";
import UserPage from "./Pages/UserPage";
import AdminLoginPage from "./Pages/AdminLoginPage";
import { BrowserRouter as Router,Route,Routes} from 'react-router-dom';

const App = () => {
    return (
        <Router>
        <Routes>
        <Route path="/" element={<Home/>}/>
        <Route path="/userpage" element={<UserPage/>}/>
        <Route path="/AdminLoginPage" element={<AdminLoginPage/>}/>
        </Routes>
        </Router>
    );
}

export default App;