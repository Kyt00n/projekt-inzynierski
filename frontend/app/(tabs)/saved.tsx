import { ActivityIndicator, FlatList, ScrollView, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import useFetch from '@/services/useFetch';
import { getSavedPosts } from '@/services/api';
import PostCard from '@/components/postCard';

const Saved = () => {
  const userId = '5';
  const {
    data: posts,
    loading: postsLoading,
    error: postsError,
  } = useFetch(()=>getSavedPosts({userId}))
  return (
    <View className="flex-1 bg-primary">
      <ScrollView className="flex-1 px-5" 
      showsVerticalScrollIndicator={false} 
      contentContainerStyle={{minHeight: "100%", paddingBottom: 10}}>
      <View className='flex mt-5 items-center flex-1 gap-5'>
        <Text className='text-4xl text-white font-bold'>💾</Text>
        <Text className='text-4xl text-white font-bold'>Saved</Text>
      </View>
        {postsLoading ? (
          <ActivityIndicator size="large" color="#0000ff" className="mt-10 self-center" />
        ):
        postsError ? (
          <Text>Error: {postsError?.message}</Text>
        ):
        <View className="flex-1 mt-5">
          <>
            <Text className="text-lg text-white font-bold mt-5 mb-3">Latest Posts</Text>

            <FlatList
              data={posts}
              keyExtractor={(item) => item.id.toString()}
              numColumns={3}
              columnWrapperStyle={{ justifyContent: "flex-start", gap:20, paddingRight: 10, marginBottom:10 }}
              className="mt-2 pb-32"
              scrollEnabled={false}
              renderItem={({ item }) => (
                <PostCard {...item}/>
              )}
              />
          </>
        </View>
          }
        
      </ScrollView>
    </View>
  );
}

export default Saved
