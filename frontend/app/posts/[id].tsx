import { ScrollView, Text, View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { useLocalSearchParams } from 'expo-router'
import { Image, ScrollView as HorizontalScrollView } from 'react-native'
import { getPostById, getUserById } from '@/services/api'

const PostDetails = () => {
    const { id } = useLocalSearchParams()

    const [post, setPost] = useState<any>(null);
    const [user, setUser] = useState<any>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
      const fetchPostAndUser = async () => {
        try {
          setLoading(true);

          // Fetch the post details
          const fetchedPost = await getPostById(id as string);
          setPost(fetchedPost);

          // Fetch the user details only after the post is fetched
          if (fetchedPost?.userId) {
            const fetchedUser = await getUserById(fetchedPost.userId.toString());
            setUser(fetchedUser);
          }
        } catch (err: any) {
          setError(err.message || 'Failed to fetch data');
        } finally {
          setLoading(false);
        }
      };

      if (id) {
        fetchPostAndUser();
      }
    }, [id]);
  return (
    <View className="bg-primary flex-1">
      <ScrollView contentContainerStyle={{paddingBottom:80}}>
        <View>
          <HorizontalScrollView horizontal showsHorizontalScrollIndicator={false} style={{ marginVertical: 10 }}>
            {post?.photos.map((photoUri: any, index: React.Key | null | undefined) => (
              <Image
                key={index}
                source={{ uri: photoUri }}
                style={{ width: 200, height: 300, marginRight: 10, borderRadius: 10 }}
                resizeMode="stretch"
                className='w-full h-[300px]'
              />
            ))}
          </HorizontalScrollView>
        </View>
        <View className="flex-col items-start justify-center mt-5 px-5">
          <Text className="text-white font-bold text-xl">{post?.title}</Text>
        </View>
        <View className="flex-row items-center gap-x-1 mt-2 ml-5">
          <Text className="text-light-200 text-m">{user?.username}</Text>
          <Text className="text-light-200 text-m">, {user?.address.city}</Text>
          <Text className='text-light-200 text-m'>, {new Date().toLocaleDateString()}</Text>
        </View>
        <View className='flex-col items-center justify-center mt-5'>
          <Text className="text-light-200 text-sm font-bold">{post?.body}</Text>
          <Text className="text-light-200 text-sm font-bold">{post?.body}</Text>
          <Text className="text-light-200 text-sm font-bold">{post?.body}</Text>
          <Text className="text-light-200 text-sm font-bold">{post?.body}</Text>
          <Text className="text-light-200 text-sm font-bold">{post?.body}</Text>
        </View>
      </ScrollView>
    </View>
  )
}

export default PostDetails
