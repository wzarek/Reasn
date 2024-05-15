import React from 'react'
import { Fire } from '../../icons/Fire'
import { HeroCard } from './HeroCard'

export const HeroSection = () => {
  return (
    <section className='w-full h-[50vh] flex justify-center items-center gap-10'>
      <div className='w-fit h-1/3 relative'>
        <div className='absolute w-2/3 h-1.5 top-0 content-[""] bg-gradient-to-r from-[#FF6363] to-[#1E34FF] rounded-lg'></div>
        <Fire className='absolute w-14 h-14 top-0 right-0 translate-y-[-50%] rotate-[16deg]' colors={['#6E45C8', '#953A3D']} gradientTransform="rotate(90)" />
        <h2 className='text-5xl h-full content-center text-right font-bold text-transparent bg-clip-text bg-gradient-to-r from-[#FF6363] to-[#1E34FF]'>
          to jest teraz <br/> na topie
        </h2>
      </div>
      <HeroCard />
    </section>
  )
}