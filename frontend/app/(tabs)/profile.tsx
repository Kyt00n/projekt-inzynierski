import { Text, View } from 'react-native'
import React from 'react'
import useFetch from '@/services/useFetch';
import { getUserById } from '@/services/api';

const Profile = () => {
  const id = '1'; 
  const {data: user, loading, error} = useFetch(() => getUserById(id as string))
  return (
    <View className='bg-primary flex-1 px-10'>
      <View className='flex mt-5 items-center flex-1 gap-5'>
        <Text className='text-4xl text-white font-bold'>👤</Text>
        <Text className='text-4xl text-white font-bold'>Profile</Text>
      </View>

      <View className='flex-1 mt-5 px-5 justify-center items-center'> 
        <Text className='text-white font-bold text-xl mt-5'>User Details</Text>
        <Text className='text-white text-l mt-2'>Name: {user?.name}</Text>
        <Text className='text-white text-l mt-2'>Username: {user?.email}</Text>
        <Text className='text-white text-l mt-2'>City: {user?.address.city}</Text>
        <Text className='text-white text-l mt-2'>Phone: {user?.phone}</Text>
      </View>
    </View>
  )
}

export default Profile