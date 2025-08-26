import './App.css';
import Login from '../src/components/pages/login.tsx';
import Register from '../src/components/pages/register.tsx';
import { Routes, Route } from 'react-router-dom';
import MainPage from './components/pages/mainpage.tsx';
import ErrorPage from './components/pages/error.tsx';
import NotFound from './components/pages/404.tsx';
import DataTableTodos  from './components/sections/todos-table.tsx';


function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/main/*" element={<MainPage />}>
        <Route index element={ <DataTableTodos /> } />
        <Route path="myday" element={
          <div className="bg-muted/50 min-h-[40vh] flex-1 rounded-xl md:min-h-min flex items-center justify-center text-xl font-heading">
            My Day Content
          </div>
        } />
        <Route path="calendar" element={
          <div className="bg-muted/50 min-h-[40vh] flex-1 rounded-xl md:min-h-min flex items-center justify-center text-xl font-heading">
            Calendar Content
          </div>
        } />
        <Route path="tags" element={
          <div className="bg-muted/50 min-h-[40vh] flex-1 rounded-xl md:min-h-min flex items-center justify-center text-xl font-heading">
            Tags Content
          </div>
        } />
        <Route path="settings" element={
          <div className="bg-muted/50 min-h-[40vh] flex-1 rounded-xl md:min-h-min flex items-center justify-center text-xl font-heading">
            Settings Content
          </div>
        } />
      </Route>
      <Route path="/error" element={<ErrorPage />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;
