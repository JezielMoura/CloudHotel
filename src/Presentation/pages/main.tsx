import React, { useState } from 'react'
import ReactDOM from 'react-dom/client'
import Calendar from './calendar/Calendar'
import './App.css'
import { Home } from './home/Home'
import { ReservationList } from './reservation/ReservationList'

function App() {
  const [currentPage, setCurrentPage] = useState('home')
  const pages = {
    'home': <Home />,
    'calendar': <Calendar />,
    'reservations': <ReservationList />,
  };

  return (
    <div className="w-full flex">
      <div className="fixed top-0 left-0 flex flex-col items-center py-8 h-screen w-20 border-r border-gray-200">
        <img src="/icons/logo.svg" alt="Home" className=" cursor-pointer h-10 rounded-full" onClick={() => setCurrentPage('home')} />
        <img src="/icons/home.svg" alt="Home" className="mt-20 cursor-pointer h-8" onClick={() => setCurrentPage('home')} />
        <img src="/icons/calendar.svg" alt="Home" className="my-10 cursor-pointer h-9" onClick={() => setCurrentPage('calendar')} />
        <img src="/icons/reservations.svg" alt="Home" className="h-8 cursor-pointer" onClick={() => setCurrentPage('reservations')} />
        <img src="/icons/room.svg" alt="Home" className="my-10 h-8 cursor-pointer" />
        <div className="absolute bottom-6">
          <img src="/icons/config.svg" alt="Home" className="h-10 cursor-pointer" />
        </div>
      </div>
      <div className="h-screen main-container pl-20">
        {pages[currentPage]} 
      </div>
    </div>
  )
}

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
)
