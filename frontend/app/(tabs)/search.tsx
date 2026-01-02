import PostCard from '@/components/postCard'
import SearchBar from '@/components/searchBar';
import { getPosts } from '@/services/api';
import useFetch from '@/services/useFetch';
import { useEffect, useState } from 'react';
import { View, Text, Image, FlatList, ActivityIndicator } from 'react-native'

const Search = () => {
    const [searchQuery, setSearchQuery] = useState('');

  const {
    data: posts,
    loading: postsLoading,
    error: postsError,
    refetch: loadPosts,
    reset,
  } = useFetch(()=>getPosts({query: searchQuery}), false)

  useEffect(() => {
    const func = async () => {
        if(searchQuery.trim()) {
        await loadPosts();
        } else {
            reset();}
    }
    func();
    }, [searchQuery])
  return (
    <View className="flex-1 bg-primary">
      <FlatList 
      data={posts} 
      renderItem={({ item }) => <PostCard {...item} />} 
      keyExtractor={(item) => item.id.toString()} 
      numColumns={3} 
      columnWrapperStyle={{ justifyContent: 'center', gap: 16, marginVertical: 16 }} 
      className="px-5" 
      contentContainerStyle={{paddingBottom:100}} 
      ListHeaderComponent={
        <>
        <View className='w-full justify-center mt-20 items-center'>
            <Text className='text-4xl'>🔍</Text>
            <Text className='text-4xl text-white font-bold ml-2'>Search</Text>
        </View>

        <View className='my-5'>
            <SearchBar placeholder='Search posts...' 
            value={searchQuery} onChangeText={(text: string) => setSearchQuery(text)}/>
        </View>

        {postsLoading && (
            <ActivityIndicator size="large" color="#0000ff" className="my-3" />
        )}
        {postsError && (
          <Text>Error: {postsError?.message}</Text>
        )}

        {!postsLoading && !postsError && searchQuery.trim() && posts?.length>0 && (
            <Text className='text-lg text-white font-bold mt-5 mb-3'>
                Search results for "{searchQuery}"
            </Text>
        )}
        </>
      }
      />
    </View>
  )
}

export default Search