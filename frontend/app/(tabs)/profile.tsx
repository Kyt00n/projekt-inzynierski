
import { Text, View, TouchableOpacity, Alert } from 'react-native'
import React from 'react'
import useFetch from '@/services/useFetch';
import { getUser } from '@/services/api';

const Profile = () => {
  const {data: user, loading, error} = useFetch(() => getUser())
  return (
    <View className='bg-primary flex-1 px-10' style={{ paddingTop: 60 }}>
      <View className='flex mt-5 items-center flex-1 gap-5'>
        <Text className='w-12 h-10 mb-4 mx-auto text-center text-4xl'>👤</Text>
        <Text className='text-4xl text-white font-bold'>Profile</Text>
      </View>
        <View className='mt-2 items-center'>
          <Text className='text-white font-bold text-2xl'>User Details</Text>
          <Text className='text-white text-lg mt-2'>Name: {user?.name}</Text>
          <Text className='text-white text-lg mt-1'>Username: {user?.email}</Text>
          <Text className='text-white text-lg mt-1'>Phone: {user?.phone}</Text>
          <Text className='text-white text-lg mt-1'>Account status: {user?.isActive ? 'Activated' : 'Not activated'}</Text>
        </View>

      <View className='flex-1 mt-5 px-5 justify-center items-center'> 
        
      <View className='pb-8 px-5'>
        <TouchableOpacity
          className='bg-blue-600 rounded-xl py-3 items-center'
          onPress={() => Alert.alert('Add or edit', 'Add or edit profile details not implemented')}
        >
          <Text className='text-white font-semibold'>Edit profile details</Text>
        </TouchableOpacity>
      </View>
      </View>
    </View>
  )
}

export default Profile