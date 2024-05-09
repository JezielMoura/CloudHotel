import React from 'react'
import ReactDOM from 'react-dom/client'
import { Calendar, loader as calendarLoader } from './calendar/Calendar'
import './App.css'
import { Home, loader as homeLoader } from './home/Home'
import { ListReservationPage, loader as reservationLoader  } from './reservations/ListReservationPage'
import { ListGuestPage, loader as guestLoader  } from './guests/ListGuestPage'
import { createBrowserRouter, RouterProvider, createRoutesFromElements, Route, useNavigate, Outlet, useNavigation } from "react-router-dom";
import { Loading } from '../components/ui/Loading'
import { CreateRoomPage, action as createRoomAction } from './rooms/CreateRoomPage'
import { ListRoomPage, loader as listRoomLoader } from './rooms/ListRoomPage'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { CreateReservationPage, loader as createReservationLoader, action as createReservationAction } from './reservations/CreateReservationPage'
import { GetReservationPage, loader as getReservationLoader, action as getReservationAction } from './reservations/GetReservationPage'
import { UpdateReservationPage, loader as updateReservationLoader, action as updateReservationAction } from './reservations/UpdateReservationPage'
import { GetGuestPage, loader as getGuestLoader } from './guests/GetGuestPage'
import { UpdateSettingsPage, loader as updateSettingsLoader, action as updateSettingsAction } from './settings/UpdateSettingsPage'

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />}>
      <Route path='/' element={<Home />} loader={homeLoader} />
      <Route path='/calendar' element={<Calendar />} loader={calendarLoader} />
      <Route path='/reservations' element={<ListReservationPage />} loader={reservationLoader} />
      <Route path='/reservations/add' element={<CreateReservationPage />} loader={createReservationLoader} action={createReservationAction} />
      <Route path='/reservations/:id' element={<GetReservationPage />} loader={getReservationLoader} action={getReservationAction} />
      <Route path='/reservations/edit/:id' element={<UpdateReservationPage />} loader={updateReservationLoader} action={updateReservationAction} />
      <Route path='/rooms' element={<ListRoomPage />} loader={listRoomLoader} />
      <Route path='/rooms/add' element={<CreateRoomPage />} action={createRoomAction} />
      <Route path='/guests' element={<ListGuestPage />} loader={guestLoader} />
      <Route path='/guests/:id' element={<GetGuestPage />} loader={getGuestLoader} />
      <Route path='/settings' element={<UpdateSettingsPage />} loader={updateSettingsLoader} action={updateSettingsAction} />
    </Route>
  )
);

function App() {
  const navigate = useNavigate();
  const navigation = useNavigation();

  return (
    <div className="w-full flex">
      <div className="fixed top-0 left-0 flex flex-col items-center py-8 h-screen w-20 border-r border-gray-200">
        <img src="/icons/logo.svg" alt="Home" className=" cursor-pointer h-10 rounded-full" onClick={() => navigate('/')} />
        <img src="/icons/home.svg" alt="Home" className="mt-20 cursor-pointer h-8" onClick={() => navigate('/')} />
        <img src="/icons/calendar.svg" alt="Home" className="my-10 cursor-pointer h-9" onClick={() => navigate('/calendar')} />
        <img src="/icons/reservations.svg" alt="Home" className="h-8 cursor-pointer" onClick={() => navigate('/reservations')} />
        <img src="/icons/room.svg" alt="Home" className="my-10 h-8 cursor-pointer" onClick={() => navigate('/rooms')} />
        <img src="/icons/guest.svg" alt="Guest" className="h-8 cursor-pointer" onClick={() => navigate('/guests')} />
        <div className="absolute bottom-6">
          <img src="/icons/config.svg" alt="Home" className="h-10 cursor-pointer" onClick={() => navigate('/settings')} />
        </div>
      </div>
      <div className="h-screen w-full pl-20 pr-5 overflow-x-hidden">
        { ['loading', 'submitting'].includes(navigation.state) && <Loading /> }
        <Outlet />
        <ToastContainer />
      </div>
    </div>
  )
}

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
)
